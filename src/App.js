import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import Pakiautomaadid from './Pakiautomaadid';
import Nordpool from './Nordpool';
import Tooted from './Tooted';
import './App.css';

function App() {
  return (
    <Router>
      <div className="App">
        <nav className="nav-bar" style={{ display: 'flex', justifyContent: 'space-around', padding: '10px', background: '#ffe5f8' }}>
          <Link to="/pakiautomaadid" style={{ textDecoration: 'none', color: 'black' }}>Pakiautomaadid</Link>
          <Link to="/nordpool" style={{ textDecoration: 'none', color: 'black' }}>Nordpool</Link>
          <Link to="/tooted" style={{ textDecoration: 'none', color: 'black' }}>Tooted</Link>
        </nav>

        <Routes>
          <Route path="/pakiautomaadid" element={<Pakiautomaadid />} />
          <Route path="/nordpool" element={<Nordpool />} />
          <Route path="/tooted" element={<Tooted />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
