(async () => {
  const [, year, month] = /clock-in\/(\d+)\/(\d+)$/.exec(
    window.location.pathname
  ) || [, null, null];

  if (!year || !month)
    throw new Error(`No year nor month information found on the current URL`);

  const employees = await fetch("https://api.factorialhr.com/employees", {
    credentials: "include",
    headers: {
      accept: "application/json, text/plain, */*",
      "accept-language": "en,es-ES;q=0.9,es;q=0.8",
    },
    referrerPolicy: "no-referrer-when-downgrade",
    body: null,
    method: "GET",
    mode: "cors",
  }).then((response) => response.json());

  const current = employees.find((x) => !!x.bank_number);

  const periods = await fetch(
    `https://api.factorialhr.com/attendance/periods?year=${year}&month=${month}&employee_id=${current.id}`,
    {
      credentials: "include",
      headers: {
        accept: "application/json, text/plain, */*",
        "accept-language": "en,es-ES;q=0.9,es;q=0.8",
        "x-factorial-access": `${current.access_id}`,
      },
      referrerPolicy: "no-referrer-when-downgrade",
      body: null,
      method: "GET",
      mode: "cors",
    }
  ).then((response) => response.json());

  const currentPeriod = periods.find((x) => x.state === "pending");

  if (!currentPeriod)
    throw new Error(`Could't find all the information necessary to update the shifts for
  this time period`);

  console.log({
    user: { id: current.id, access_id: current.access_id },
    month,
    year,
    period_id: currentPeriod.id,
  });

  function sendShift(day, clock_in, clock_out) {
    return fetch("https://api.factorialhr.com/attendance/shifts", {
      credentials: "include",
      headers: {
        accept: "application/json, text/plain, */*",
        "accept-language": "en,es-ES;q=0.9,es;q=0.8",
        "content-type": "application/json;charset=UTF-8",
        "x-factorial-access": `${current.access_id}`,
      },
      referrerPolicy: "no-referrer-when-downgrade",
      body: JSON.stringify({
        period_id: Number(currentPeriod.id),
        clock_in,
        clock_out,
        minutes: 0,
        day,
        observations: null,
        history: [],
      }),
      method: "POST",
      mode: "cors",
    });
  }

  const currentDate = new Date();
  const rowDate = new Date(Number(year), Number(month) - 1, 1);

  const allRows = document.querySelectorAll(
    '[class^="tableContainer"] table tbody tr'
  );
  const filled = [];
  const empty = [];

  function getMonthDayElement(element) {
    return element.querySelector('[class^="monthDay"]');
  }

  function getRowInfo(element) {
    const monthDayElement = getMonthDayElement(element);
    const dayOfMonth = /^\d+/.exec(monthDayElement.textContent)[0];
    rowDate.setDate(Number(dayOfMonth));
    const dayOfWeek = rowDate.getDay();
    const extraTags = Array.from(monthDayElement.parentElement.children)
      .slice(2)
      .map((x) => x.textContent);
    const hoursElement = Array.from(
      element.querySelectorAll("td")
    ).find((element) => /^(\d+)h$/.test(element.textContent));
    const hours = hoursElement
      ? /^(\d+)h$/.exec(hoursElement.textContent)[1]
      : "0";
    return {
      element,
      dayOfMonth,
      isInFuture: currentDate < rowDate,
      isWeekend: dayOfWeek % 6 === 0 /* Either 0 or 6 (Sunday or Saturday respectively) */,
      extraTags,
      hours,
    };
  }

  allRows.forEach((element) => {
    const rowInfo = getRowInfo(element);
    if (Number(rowInfo.hours) > 0) {
      filled.push(rowInfo);
    } else if (
      !rowInfo.isWeekend &&
      !rowInfo.isInFuture &&
      rowInfo.extraTags.length === 0
    ) {
      empty.push(rowInfo);
    }
  });

  function updateEmptyRowsShifts(shifts) {
    empty
      .reduce((acc, { dayOfMonth }) => {
        return shifts.reduce(
          (acc, [clock_in, clock_out]) =>
            acc.then(() => sendShift(dayOfMonth, clock_in, clock_out)),
          acc
        );
      }, new Promise((resolve) => resolve()))
      .catch((e) => {
        console.error(e);
        alert(JSON.stringify(e));
      })
      .then(() => location.reload());
  }

  empty.forEach(({ element }) => {
    element.style.backgroundColor = `rgba(256, 0, 0, 0.1)`;
  });

  filled.forEach(({ element: filledRowElement }) => {
    const element = filledRowElement.querySelector("td:nth-child(2)");
    const el = document.createElement("button");
    el.innerText = "Copy on empty days";
    element.appendChild(el);
    el.addEventListener("click", () => {
      if (empty.length) {
        const shifts = [...element.querySelectorAll("input")].reduce(
          (acc, input, index) => {
            if (index % 2 === 0) acc.push([input.value]);
            else acc[acc.length - 1].push(input.value);
            return acc;
          },
          []
        );

        updateEmptyRowsShifts(shifts);
      }
    });
  });
})();
