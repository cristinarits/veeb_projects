import React, { useEffect, useState } from 'react';
import './App.css';

function App() {
  const [testiHinded, setTestiHinded] = useState([]);
  const [filteredTestiHinded, setFilteredTestiHinded] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [newTestScore, setNewTestScore] = useState({ saadudPunktid: '', maksPunktid: '', comment: '', subject: '', date: '' });
  const [filter, setFilter] = useState({ subject: '', date: '' });

  useEffect(() => {
    fetchTestiHinded();
  }, []);

  useEffect(() => {
    applyFilter();
  }, [testiHinded, filter]);

  const fetchTestiHinded = async () => {
    try {
      const response = await fetch('https://localhost:7257/TestScores');
      if (!response.ok) {
        throw new Error('Võrgu vastus ei olnud korras');
      }
      const data = await response.json();
      setTestiHinded(data);
      setLoading(false);
    } catch (error) {
      setError(error);
      setLoading(false);
    }
  };

  const deleteTestScore = async (id) => {
    try {
      const response = await fetch(`https://localhost:7257/TestScores/${id}`, {
        method: 'DELETE',
      });
      if (!response.ok) {
        throw new Error('Testihinde kustutamine ebaõnnestus');
      }
      setTestiHinded(testiHinded.filter(hinne => hinne.id !== id));
    } catch (error) {
      setError(error);
    }
  };

  const addTestScore = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch('https://localhost:7257/TestScores', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(newTestScore)
      });
      if (!response.ok) {
        throw new Error('Testihinde lisamine ebaõnnestus');
      }
      const addedScore = await response.json();
      setTestiHinded([...testiHinded, addedScore]);
      setNewTestScore({ saadudPunktid: '', maksPunktid: '', comment: '', subject: '', date: '' });
    } catch (error) {
      setError(error);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setNewTestScore(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleFilterChange = (e) => {
    const { name, value } = e.target;
    setFilter(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const applyFilter = () => {
    let filtered = testiHinded;
    if (filter.subject) {
      filtered = filtered.filter(hinne => hinne.subject.toLowerCase().includes(filter.subject.toLowerCase()));
    }
    if (filter.date) {
      filtered = filtered.filter(hinne => hinne.date.includes(filter.date));
    }
    setFilteredTestiHinded(filtered);
  };

  if (loading) return <p>Laadimine...</p>;
  if (error) return <p>Viga: {error.message}</p>;

  return (
    <div className="App">
      <h1>Testi Hinded</h1>

      <div className="filter">
        <input
          type="text"
          placeholder="Filtreeri aine järgi"
          name="subject"
          value={filter.subject}
          onChange={handleFilterChange}
        />
        <input
          type="date"
          placeholder="Filtreeri kuupäeva järgi"
          name="date"
          value={filter.date}
          onChange={handleFilterChange}
        />
      </div>

      <ul>
        {filteredTestiHinded.map(hinne => (
          <li key={hinne.id}>
            <div>
              <strong>Aine:</strong> {hinne.subject}<br />
              <strong>Punktid:</strong> {hinne.saadudPunktid}/{hinne.maksPunktid} - {hinne.protsent}%<br />
              <strong>Hinne:</strong> {hinne.hinne}<br />
              <strong>Kommentaar:</strong> {hinne.comment}<br />
              <strong>Kuupäev:</strong> {hinne.date}
            </div>
            <button onClick={() => deleteTestScore(hinne.id)}>Kustuta</button>
          </li>
        ))}
      </ul>

      <form onSubmit={addTestScore}>
        <div>
          <label>
            Aine:
            <input
              type="text"
              name="subject"
              value={newTestScore.subject}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <div>
          <label>
            Saadud Punktid:
            <input
              type="number"
              name="saadudPunktid"
              value={newTestScore.saadudPunktid}
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
              value={newTestScore.maksPunktid}
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
              value={newTestScore.comment}
              onChange={handleChange}
            />
          </label>
        </div>
        <div>
          <label>
            Kuupäev:
            <input
              type="date"
              name="date"
              value={newTestScore.date}
              onChange={handleChange}
              required
            />
          </label>
        </div>
        <button type="submit">Lisa Testi Hinne</button>
      </form>
    </div>
  );
}

export default App;