import React, { useEffect, useState } from 'react';

function Nordpool() {
  const [fi, setFi] = useState([]);
  const [ee, setEe] = useState([]);
  const [lv, setLv] = useState([]);
  const [lt, setLt] = useState([]);

  useEffect(() => {
    fetch("https://localhost:4444/nordpool")
      .then(res => res.json())
      .then(json => {
        setFi(json.data.fi);
        setEe(json.data.ee);
        setLv(json.data.lv);
        setLt(json.data.lt);
      });
  }, []);

  return (
    <div style={{ textAlign: 'center' }}>
      <h1>Nordpool</h1>
      <table style={{ margin: '0 auto', borderCollapse: 'collapse', width: '30%' }}>
        <thead>
          <tr>
            <th style={{ border: '1px solid #000000', padding: '12px', backgroundColor: '#ff8fde'}}>Ajatempel</th>
            <th style={{ border: '1px solid #000000', padding: '12px', backgroundColor: '#ff8fde'}}>Hind</th>
          </tr>
        </thead>
        <tbody>
          <tr>
            <td colSpan="2" style={{ textAlign: 'left', fontWeight: 'bold', backgroundColor: '#fdbaf8', border: '1px solid #000000', paddingLeft: '200px' }}>Soome</td>
          </tr>
          {fi.map(data =>
            <tr key={data.timestamp}>
              <td style={{ border: '1px solid #000000', padding: '8px' }}>{new Date(data.timestamp * 1000).toISOString()}</td>
              <td style={{ border: '1px solid #000000', padding: '8px' }}>{data.price}</td>
            </tr>)}
          <tr>
            <td colSpan="2" style={{ textAlign: 'left', fontWeight: 'bold', backgroundColor: '#fdbaf8', border: '1px solid #000000', paddingLeft: '200px' }}>Eesti</td>
          </tr>
          {ee.map(data =>
            <tr key={data.timestamp}>
              <td style={{ border: '1px solid #000000', padding: '8px' }}>{new Date(data.timestamp * 1000).toISOString()}</td>
              <td style={{ border: '1px solid #000000', padding: '8px' }}>{data.price}</td>
            </tr>)}
          <tr>
            <td colSpan="2" style={{ textAlign: 'left', fontWeight: 'bold', backgroundColor: '#fdbaf8', border: '1px solid #000000', paddingLeft: '200px' }}>Läti</td>
          </tr>
          {lv.map(data =>
            <tr key={data.timestamp}>
              <td style={{ border: '1px solid #000000', padding: '8px' }}>{new Date(data.timestamp * 1000).toISOString()}</td>
              <td style={{ border: '1px solid #000000', padding: '8px' }}>{data.price}</td>
            </tr>)}
          <tr>
            <td colSpan="2" style={{ textAlign: 'left', fontWeight: 'bold', backgroundColor: '#fdbaf8', border: '1px solid #000000', paddingLeft: '200px' }}>Leedu</td>
          </tr>
          {lt.map(data =>
            <tr key={data.timestamp}>
              <td style={{ border: '1px solid #000000', padding: '8px' }}>{new Date(data.timestamp * 1000).toISOString()}</td>
              <td style={{ border: '1px solid #000000', padding: '8px' }}>{data.price}</td>
            </tr>)}
        </tbody>
      </table>
    </div>
  );
}

export default Nordpool;
