import React, { useEffect, useState } from 'react';

function Pakiautomaadid() {
  const [pakiautomaadid, setPakiautomaadid] = useState([]);

  useEffect(() => {
    fetch("https://localhost:4444/parcelmachine")
      .then(res => res.json())
      .then(json => setPakiautomaadid(json))
      .catch(err => console.error("Error fetching parcel machine data:", err));
  }, []);

  return (
    <div>
      <h1>Pakiautomaadid</h1>
      <select>
        {pakiautomaadid.map(automaat => 
          <option key={automaat.NAME}>
            {automaat.NAME}
          </option>)}
      </select>
    </div>
  );
}

export default Pakiautomaadid;