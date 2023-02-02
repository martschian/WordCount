import React, { Component } from 'react';

export default class App extends Component {
  static displayName = App.name;

  constructor(props) {
    super(props);
    this.state = { projects: [], loading: true };
  }

  componentDidMount() {
    this.populateWeatherData();
  }

  static renderprojectsTable(projects) {
    return (
      <table className="table table-striped" aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>ID</th>
            <th>Title</th>

            <th>Description</th>
          </tr>
        </thead>
        <tbody>
          {projects.map((project) => (
            <tr key={project.id}>
              <td>{project.id}</td>
              <td>{project.title}</td>

              <td>{project.description}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>
          Loading... Please refresh once the ASP.NET backend has started. See{' '}
          <a href="https://aka.ms/jspsintegrationreact">
            https://aka.ms/jspsintegrationreact
          </a>{' '}
          for more details.
        </em>
      </p>
    ) : (
      App.renderprojectsTable(this.state.projects)
    );

    return (
      <div>
        <h1 id="tabelLabel">Projects</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateWeatherData() {
    const response = await fetch('/api/projects/');
    const data = await response.json();
    this.setState({ projects: data, loading: false });
  }
}
