import React, { useEffect, useRef, useState } from 'react';

function Tooted() {
  const [tooted, setTooted] = useState([]);
  const idRef = useRef();
  const nameRef = useRef();
  const priceRef = useRef();
  const isActiveRef = useRef();

  useEffect(() => {
    fetch("https://localhost:4444/tooted")
      .then(res => res.json())
      .then(json => setTooted(json))
      .catch(err => console.error("Error fetching tooted data:", err));
  }, []);

  function kustuta(index) {
    fetch("https://localhost:4444/tooted/kustuta/" + index, { method: "DELETE" })
      .then(res => res.json())
      .then(json => setTooted(json))
      .catch(err => console.error("Error deleting toode:", err));
  }

  function lisa() {
    const uusToode = {
      id: Number(idRef.current.value),
      name: nameRef.current.value,
      price: Number(priceRef.current.value),
      isActive: isActiveRef.current.checked
    };
    fetch("https://localhost:4444/tooted/lisa", {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(uusToode)
    })
      .then(res => res.json())
      .then(json => setTooted(json))
      .catch(err => console.error("Error adding toode:", err));
  }

  function dollariteks() {
    const kurss = 1.1;
    fetch("https://localhost:4444/tooted/hind-dollaritesse/" + kurss, { method: "PATCH" })
      .then(res => res.json())
      .then(json => setTooted(json))
      .catch(err => console.error("Error converting prices to dollars:", err));
  }

  return (
    <div>
      <h1>Tooted</h1>
      <label>ID</label> <br />
      <input ref={idRef} type="number" /> <br />
      <label>Nimi</label> <br />
      <input ref={nameRef} type="text" /> <br />
      <label>Hind</label> <br />
      <input ref={priceRef} type="number" /> <br />
      <label>Aktiivne</label> <br />
      <input ref={isActiveRef} type="checkbox" /> <br />
      <button onClick={lisa}>Lisa</button>
      <button onClick={dollariteks}>Muuda dollariteks</button>
      {tooted.map((toode, index) => 
        <div key={index}>
          <div>{toode.id}</div>
          <div>{toode.name}</div>
          <div>{toode.price}</div>
          <button onClick={() => kustuta(index)}>x</button>
        </div>)}
    </div>
  );
}

export default Tooted;