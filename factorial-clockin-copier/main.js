(() => {
  const [, year, month] = /clock-in\/(\d+)\/(\d+)$/.exec(
    window.location.pathname
  ) || [, null, null];

  if (year && month) {
    fetch("https://api.factorialhr.com/employees", {
      credentials: "include",
      headers: {
        accept: "application/json, text/plain, */*",
        "accept-language": "en,es-ES;q=0.9,es;q=0.8"
      },
      referrerPolicy: "no-referrer-when-downgrade",
      body: null,
      method: "GET",
      mode: "cors"
    })
      .then(response => response.json())
      .then(employees => {
        const current = employees.find(x => !!x.bank_number);
        return fetch(
          `https://api.factorialhr.com/attendance/periods?year=${year}&month=${month}&employee_id=${current.id}`,
          {
            credentials: "include",
            headers: {
              accept: "application/json, text/plain, */*",
              "accept-language": "en,es-ES;q=0.9,es;q=0.8",
              "x-factorial-access": `${current.access_id}`
            },
            referrerPolicy: "no-referrer-when-downgrade",
            body: null,
            method: "GET",
            mode: "cors"
          }
        )
          .then(response => response.json())
          .then(periods => {
            const currentPeriod = periods.find(x => x.state === "pending");
            if (currentPeriod) {
              console.log({
                user: { id: current.id, access_id: current.access_id },
                month,
                year,
                period_id: currentPeriod.id
              });
              function sendShift(day, clock_in, clock_out) {
                return fetch("https://api.factorialhr.com/attendance/shifts", {
                  credentials: "include",
                  headers: {
                    accept: "application/json, text/plain, */*",
                    "accept-language": "en,es-ES;q=0.9,es;q=0.8",
                    "content-type": "application/json;charset=UTF-8",
                    "x-factorial-access": `${current.access_id}`
                  },
                  referrerPolicy: "no-referrer-when-downgrade",
                  body: JSON.stringify({
                    period_id: Number(currentPeriod.id),
                    clock_in,
                    clock_out,
                    minutes: 0,
                    day,
                    observations: null,
                    history: []
                  }),
                  method: "POST",
                  mode: "cors"
                });
              }

              const [filled, empty] = [
                ...document.querySelectorAll(
                  '[class^="tableContainer"] table tbody tr'
                )
              ].reduce(
                ([filled, empty, hasTodayBeenPassed], element) => {
                  if (
                    !hasTodayBeenPassed &&
                    element.className.indexOf("disabled") < 0
                  ) {
                    var hasHoursFilled = [
                      ...element.querySelectorAll("td")
                    ].some(
                      tdElement =>
                        (/(\d+)h/.exec(tdElement.textContent) || [])[1] > 0
                    );
                    if (hasHoursFilled) {
                      filled.push(element);
                    } else {
                      empty.push(element);
                    }
                  }
                  return [
                    filled,
                    empty,
                    hasTodayBeenPassed ||
                      element
                        .querySelector('[class^="monthDay"]')
                        .className.indexOf("isToday") >= 0
                  ];
                },
                [[], [], false]
              );

              filled.forEach(rowElement => {
                const element = rowElement.querySelector("td:nth-child(2)");
                const el = document.createElement("button");
                el.innerText = "Copy on empty days";
                element.appendChild(el);
                el.addEventListener("click", () => {
                  if (empty.length) {
                    const shifts = [
                      ...element.querySelectorAll("input")
                    ].reduce((acc, input, index) => {
                      if (index % 2 === 0) acc.push([input.value]);
                      else acc[acc.length - 1].push(input.value);
                      return acc;
                    }, []);

                    empty
                      .reduce((acc, rowElement) => {
                        const day = Number(
                          /^\d+/.exec(
                            rowElement.querySelector('[class^="monthDay"]')
                              .innerText
                          )[0]
                        );
                        return shifts.reduce(
                          (acc, [clock_in, clock_out]) =>
                            acc.then(() => sendShift(day, clock_in, clock_out)),
                          acc
                        );
                      }, new Promise(resolve => resolve()))
                      .catch(e => {
                        console.error(e);
                        alert(JSON.stringify(e));
                      })
                      .then(() => location.reload());
                  }
                });
              });
            }
          });
      });
  } else {
    alert(`Couldn't get the current year and/or month`);
  }
})();
