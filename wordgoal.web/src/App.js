import React from 'react';
import { Route, Routes } from 'react-router-dom';
import HomePage from './home/HomePage';
import NotesPage from './notes/NotesPage';

function App() {
  return (
    <div className="container-fluid">
      <Routes>
        <Route exact path="/" element={<HomePage />} />
        <Route path="/notes" element={<NotesPage />} />
        {/* <Route path="/notes/:slug" element={<NotesPage />} /> */}
      </Routes>
    </div>
  );
}

export default App;
