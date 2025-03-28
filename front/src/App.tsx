import './App.css';
import { Route, Routes } from 'react-router-dom';
import HomePage from './home/HomePage';

const App = () => {
  return (
    <>
    <Routes>
      <Route path="/" element={<HomePage />} />
      {/* <Route path="/game" element={<Game />} />
      <Route path="/rank" element={<Rank />} />
      <Route path="/profile" element={<Profile />} /> */}
    </Routes>
    </>
  )
}

export default App;
