(async () => {
  let currentSelectionData = null;

  const getSelectionData = async (targetProjectId) => {
    const data = {
      myEmployeeId: null,
      monthInit: null,
      monthEnd: null,
      daysToUpdate: [],
    };

    data.myEmployeeId =
      window.location.href.split("employee/")[1]?.split("/")[0] ?? null;

    if (!data.myEmployeeId) {
      console.log("Not found employeeId");
      return null;
    }

    const allowedDays = Array.from(
      document.querySelectorAll(`[data-test-id^="day_"]`)
    )
      .filter(
        (x) =>
          !x.textContent.includes("Approved") &&
          x.querySelector(`[data-test-id="day-menu"]`)
      )
      .map((x) => x.getAttribute("data-test-id").replace("day_", ""));

    if (!allowedDays.length) {
      console.log("No date range info found");
      return null;
    }

    data.monthInit = allowedDays[0];
    data.monthEnd = allowedDays[allowedDays.length - 1];

    const fetchPeriods = () =>
      fetch(
        `https://zartis.personio.de/api/v1/attendances/periods?filter[startDate]=${data.monthInit}&filter[endDate]=${data.monthEnd}&filter[employee]=${data.myEmployeeId}`,
        {
          headers: {
            accept: "application/json, text/plain, */*",
            "accept-language": "en,es-ES;q=0.9,es;q=0.8",
          },
          referrerPolicy: "strict-origin-when-cross-origin",
          body: null,
          method: "GET",
          mode: "cors",
          credentials: "include",
        }
      )
        .then((x) => x.json())
        .then((x) => x.data ?? [])
        .then((data) =>
          data
            .filter((x) => x.type === "attendance-period")
            .sort((a, b) =>
              a.attributes.start.localeCompare(b.attributes.start)
            )
        );

    const periods = await fetchPeriods();

    data.daysToUpdate = Array.from(
      periods
        .reduce((acc, period) => {
          const {
            id,
            attributes: {
              attendance_day_id,
              project_id,
              period_type,
              legacy_break_min,
              comment,
              start,
              end,
            },
          } = period;

          const newPeriod = {
            id,
            project_id,
            period_type,
            legacy_break_min,
            comment,
            start,
            end,
          };

          const current = acc.get(attendance_day_id) ?? {
            dayId: attendance_day_id,
            dayString: start.substring(0, 10),
            periods: [],
          };

          current.periods.push(newPeriod);

          acc.set(attendance_day_id, current);

          return acc;
        }, new Map())
        .values()
    ).reduce((acc, dayInfo) => {
      if (
        allowedDays.includes(dayInfo.dayString) &&
        dayInfo.periods.find(
          (x) => x.period_type === "work" && x.project_id != targetProjectId
        )
      ) {
        dayInfo.periods.forEach((x) => {
          if (x.period_type === "work") {
            x.project_id = targetProjectId;
          }
        });
        acc.push(dayInfo);
      }
      return acc;
    }, []);

    return data;
  };

  const updateSelectedData = async (data, onProgress) => {
    let daysUpdated = 0;
    await data.daysToUpdate
      .reduce(
        (acc, dayToUpdate, index) =>
          acc.then(() => {
            return fetch(
              `https://zartis.personio.de/api/v1/attendances/days/${dayToUpdate.dayId}`,
              {
                headers: {
                  accept: "application/json, text/plain, */*",
                  "accept-language": "en,es-ES;q=0.9,es;q=0.8",
                  "content-type": "application/json",
                },
                referrerPolicy: "strict-origin-when-cross-origin",
                body: JSON.stringify({
                  employee_id: data.myEmployeeId,
                  periods: dayToUpdate.periods,
                }),
                method: "PUT",
                mode: "cors",
                credentials: "include",
              }
            )
              .then((x) => {
                if (!x.ok) {
                  console.log(x);
                } else {
                  daysUpdated += 1;
                }
              })
              .catch((error) => console.error(`[${index}] failed`, error))
              .then(() => {
                onProgress?.("progress", index + 1, data.daysToUpdate.length);
                return new Promise((resolve) => setTimeout(resolve, 1000));
              });
          }),
        Promise.resolve()
      )
      .then(() => {
        onProgress?.("end", daysUpdated, data.daysToUpdate.length);
      });
  };

  const projects = await fetch(
    "https://zartis.personio.de/api/v1/projects?filter[active]=1",
    {
      headers: {
        accept: "application/json, text/plain, */*",
        "accept-language": "en,es-ES;q=0.9,es;q=0.8",
      },
      referrerPolicy: "strict-origin-when-cross-origin",
      method: "GET",
      mode: "cors",
      credentials: "include",
    }
  )
    .then((x) => x.json())
    .then((x) => x.data.map((x) => ({ id: x.id, name: x.attributes.name })));

  const uiElement = document.createElement("div");
  uiElement.setAttribute(
    "style",
    `border: 1px solid black; 
      width: 300px;
      position: fixed; 
      top: 16px; 
      right: 16px; 
      border-radius: 4px; 
      z-index: 9999; 
      background-color: white;
      padding: 16px;`
  );
  uiElement.innerHTML = `<h3>Assign project</h3>
      <p>Just get on the month page you want to update, select the right project and click "assign". it will only update those with no (or other) project.</p>
      <button id="__ap-close__" style="position: absolute; top: 0; right: 0; background-color: black; color: white; padding: 8px; border-radius: 100%;" type="button">X</button>
      <div id="__ap-check__">
        <select name="projects"></select><button type="button" name="check">Check</button>
      </div>
      <div id="__ap-selection__" style="display:none">
        <p></p>
        <button type="button">Assign</button><button type="button">Reset</button>
      </div>
    `;
  document.body.appendChild(uiElement);

  const dropdown = uiElement.querySelector("select");

  projects.forEach((project) => {
    const option = document.createElement("option");
    option.setAttribute("value", project.id);
    option.textContent = project.name;

    dropdown.appendChild(option);
  });

  dropdown.value = projects[0]?.id ?? "";

  const rerenderSelectionRelatedElements = () => {
    const selectionElement = uiElement.querySelector("#__ap-selection__");

    document.querySelectorAll(`[data-test-id^="day_"]`).forEach((x) => {
      x.style.boxShadow = "";
    });

    if (!currentSelectionData) {
      selectionElement.style.display = "none";
      return;
    }
    selectionElement.style.display = "";
    selectionElement.querySelector(
      "p"
    ).textContent = `Found ${currentSelectionData.daysToUpdate.length} days to update on the range from ${currentSelectionData.monthInit} to ${currentSelectionData.monthEnd}`;

    currentSelectionData.daysToUpdate.forEach(({ dayString }) => {
      document.querySelector(
        `[data-test-id="day_${dayString}"]`
      ).style.boxShadow = "inset -1px 0px 5px 0px #FF0267";
    });

    selectionElement.querySelectorAll("button").forEach((x) => {
      x.style.display = currentSelectionData.daysToUpdate.length ? "" : "none";
    });
  };

  uiElement.querySelector("#__ap-close__").addEventListener("click", () => {
    currentSelectionData = null;
    rerenderSelectionRelatedElements();
    document.body.removeChild(uiElement);
  });

  dropdown.addEventListener("change", () => {
    currentSelectionData = null;
    rerenderSelectionRelatedElements();
  });

  const runAsyncProcess = async (func) => {
    uiElement.querySelectorAll("select, button").forEach((element) => {
      element.disabled = true;
    });

    try {
      await func();
    } catch (error) {
      console.log(error);
      alert(error.toString());
    }

    uiElement.querySelectorAll("select, button").forEach((element) => {
      element.disabled = false;
    });
  };

  uiElement
    .querySelector("#__ap-check__ > button")
    .addEventListener("click", () =>
      runAsyncProcess(async () => {
        const progressElement = uiElement.querySelector(".progress");

        const data = await getSelectionData(Number(dropdown.value));
        currentSelectionData = data;

        rerenderSelectionRelatedElements();
        return;

        if (
          data.daysToUpdate.length &&
          confirm(
            `Do you want to update ${data.daysToUpdate.length} from ${data.monthInit} to ${data.monthEnd}`
          )
        ) {
          progressElement.textContent = `Updating ${data.daysToUpdate.length} days`;

          await updateSelectedData(data, (event, a, b) => {
            switch (event) {
              case "progress":
                progressElement.textContent = `${a}/${b} (${Math.round(
                  (a / b) * 100
                )}%)`;
                break;
              case "end":
                progressElement.textContent = `${a} of ${b} days where updated successfully on the last process`;
            }
          });
        }
      })
    );

  const [assignButton, resetButton] = Array.from(
    uiElement.querySelectorAll(`#__ap-selection__ > button`)
  );

  assignButton.addEventListener("click", () => {
    if (currentSelectionData) {
      const progressElement = uiElement.querySelector("#__ap-selection__ > p");
      updateSelectedData(currentSelectionData, (event, a, b) => {
        switch (event) {
          case "progress":
            progressElement.textContent = `${a}/${b} (${Math.round(
              (a / b) * 100
            )}%)`;
            break;
          case "end":
            progressElement.textContent = `${a} of ${b} days where updated successfully on the last process`;
        }
      });
    }
  });

  resetButton.addEventListener("click", () => {
    currentSelectionData = null;
    rerenderSelectionRelatedElements();
  });
})();
