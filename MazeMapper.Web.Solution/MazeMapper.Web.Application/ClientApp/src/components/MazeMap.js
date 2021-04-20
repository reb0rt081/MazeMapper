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
        const rows = mazeInput.split("\n");
        const listItems = rows.map((row) =>
            <p>{row}</p>
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
        <h1 id="tabelLabel" >Maze solver</h1>
        <p>This component demonstrates how to solve maze problems.</p>
        {contents}
      </div>
    );
  }

  async populateMazeData() {
    const response = await fetch('mazemapper');
      const data = await response.text();
    this.setState({ maze: data, loading: false });
    }

//fetch('https://mywebsite.com/endpoint/', {
//    method: 'POST',
//    headers: {
//        'Accept': 'application/json',
//        'Content-Type': 'application/json',
//    },
//    body: JSON.stringify({
//        firstParam: 'yourValue',
//        secondParam: 'yourOtherValue',
//    })
//})

}
