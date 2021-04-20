import React, { Component } from 'react';

export class MazeMap extends Component {
    static displayName = MazeMap.name;

  constructor(props) {
    super(props);
    this.state = { maze: "", loading: true };
  }

  componentDidMount() {
    this.populateMazeData();
  }

    static showResult(mazeInput) {
        const numbers = mazeInput.split("\n");
        const listItems = numbers.map((number) =>
            <p>{number}</p>
    );
    return (
    <div>{listItems}</div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : MazeMap.showResult(this.state.maze);

    return (
      <div>
        <h1 id="tabelLabel" >Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async populateMazeData() {
    const response = await fetch('mazemapper');
      const data = await response.text();
    this.setState({ maze: data, loading: false });
  }
}
