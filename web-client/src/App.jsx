/* eslint-disable react/prop-types */
// see server side for typing
import { useEffect, useState, useMemo } from 'react'
import 'rc-slider/assets/index.css';
import './App.css'
import Slider from 'rc-slider';
import SliderSignals from "./signals/sliderUpdate";
import config from "./config"

function SessionSlider({sessionObj: session}) {
  let onChange = vol => SliderSignals.onUpdate(
    Object.assign(session, { volume: vol })
  );

  

  return (
    <>
      <div className="session-slider">
        <Slider defaultValue={session.volume} vertical={true} onChange={onChange} />
        <h1>{session.name}</h1>
      </div>
    </>
  )
}

function App() {
  const [sessions, setSessions] = useState([]);

  useEffect(() => {
    console.log(`${config.server}/sessions`)
    fetch(`${config.server}/sessions`)
      .then(res => res.json())
      .then(s => setSessions(s));
  }, []);

  return (
    <>
      <div className="sliders">
        <p>test</p>
        {sessions.map(session => (
          <SessionSlider key={session.processId} sessionObj={session} />
        ))}
      </div>
    </>
  );
}

export default App
