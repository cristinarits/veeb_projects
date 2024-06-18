import React, { useEffect, useState } from 'react';
import './App.css';

function App() {
  const [testiHinded, setTestiHinded] = useState([]);
  const [laadimine, setLaadimine] = useState(true);
  const [viga, setViga] = useState(null);
  const [uusHinne, setUusHinne] = useState({ saadudPunktid: '', maksPunktid: '', comment: '' });

  useEffect(() => {
    tooTestiHinded();
  }, []);

  const tooTestiHinded = async () => {
    try {
      const response = await fetch('https://localhost:7257/TestScores');
      if (!response.ok) {
        throw new Error('Võrguvastus ei olnud korras');
      }
      const data = await response.json();
      setTestiHinded(data);
      setLaadimine(false);
    } catch (error) {
      setViga(error);
      setLaadimine(false);
    }
  };

  const kustutaTestiHinne = async (id) => {
    try {
      const response = await fetch(`https://localhost:7257/TestScores/${id}`, {
        method: 'DELETE',
      });
      if (!response.ok) {
        throw new Error('Testihinde kustutamine ebaõnnestus');
      }
      setTestiHinded(testiHinded.filter(hinne => hinne.id !== id));
    } catch (error) {
      setViga(error);
    }
  };

  const lisaTestiHinne = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch('https://localhost:7257/TestScores', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          saadudPunktid: uusHinne.saadudPunktid,
          maksPunktid: uusHinne.maksPunktid,
          comment: uusHinne.comment
        })
      });
      if (!response.ok) {
        throw new Error('Testihinde lisamine ebaõnnestus');
      }
      const newScore = await response.json();
      setTestiHinded([...testiHinded, newScore]);
      setUusHinne({ saadudPunktid: '', maksPunktid: '', comment: '' });
    } catch (error) {
      setViga(error);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setUusHinne(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  if (laadimine) return <p>Laadimine...</p>;
  if (viga) return <p>Viga: {viga.message}</p>;

  return (
    <div className="App">
      <h1>TULEMUSED</h1>
      <ul>
        {testiHinded.map(hinne => (
          <li key={hinne.id}>
            {hinne.saadudPunktid}/{hinne.maksPunktid} - {hinne.protsent}% : {hinne.hinne} <br />
            Kommentaar: {hinne.comment}
            <button onClick={() => kustutaTestiHinne(hinne.id)}>x</button>
          </li>
        ))}
      </ul>
      <form onSubmit={lisaTestiHinne}>
        <div>
          <label>
            Saadud Punktid:
            <input
              type="number"
              name="saadudPunktid"
              value={uusHinne.saadudPunktid}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            Maksimum Punktid:
            <input
              type="number"
              name="maksPunktid"
              value={uusHinne.maksPunktid}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            Kommentaar:
            <input
              type="text"
              name="comment"
              value={uusHinne.comment}
              onChange={handleChange}
            />
          </label>
        </div>
        <button type="submit">Arvuta hinne</button>
      </form>
    </div>
  );
}

export default App;
