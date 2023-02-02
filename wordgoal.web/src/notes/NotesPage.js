import { useEffect, useState } from 'react';

const NotesPage = () => {
  const [notes, setNotes] = useState([]);
  useEffect(() => {
    const fetchData = async () => {
      const response = await fetch('/api/projects/2/notes');
      const data = await response.json();
      setNotes(data);
    };

    fetchData();
  }, []);

  return (
    <table className="table table-striped" aria-labelledby="tabelLabel">
      <thead>
        <tr>
          <th>ID</th>
          <th>Title</th>

          <th>Description</th>
          <th>Text</th>
        </tr>
      </thead>
      <tbody>
        {notes.map((note) => (
          <tr key={note.id}>
            <td>{note.id}</td>
            <td>{note.title}</td>

            <td>{note.description}</td>
            <td>{note.noteText}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default NotesPage;
