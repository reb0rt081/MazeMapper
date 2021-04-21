import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { NavLink } from 'reactstrap';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
        <div>
        <h1>Hello, world!</h1>
        <p>Welcome to my new web page application, built with the following elements:</p>
        <ul>
        <li><a href='https://get.asp.net/'>ASP.NET Core</a> and <a href='https://msdn.microsoft.com/en-us/library/67ef8sbd.aspx'>C#</a> for cross-platform server-side code</li>
        <li><a href='https://facebook.github.io/react/'>React</a> for client-side code</li>
        <li><a href='http://getbootstrap.com/'>Bootstrap</a> for layout and styling</li>
        </ul>
        <p>For more information about me you can find <a href='https://www.linkedin.com/in/robertoribes'>my LinkedIn profile</a>:</p>
        <h1>Application guidelines</h1>
        <h2>Architectural pattern</h2>
        <p>This page application follows a client-server pattern as follows:</p>
        <ul>
        <li><strong>Client-side navigation</strong>. For example, you can click <em>MazeMap</em> then <em>Back</em> to return here across the page.</li>
        <li><strong>Development server integration</strong>. In development mode, the development server from <code>create-react-app</code> runs in the background automatically, so the client-side resources are dynamically built on demand and the page refreshes when a any file is modified.</li>
        <li><strong>Efficient production builds</strong>. In production mode, development-time features are disabled, and the <code>dotnet publish</code> configuration produces minified, efficiently bundled JavaScript files.</li>
        </ul>
        <p>The <code>ClientApp</code> subdirectory is a standard React application based on the <code>create-react-app</code> template. If a command prompt gets open in that directory, <code>npm</code> commands can be run such as <code>npm test</code> or <code>npm install</code>.</p>
        <h2>Instructions in order to use this app</h2>
        <p>Navigate to the <strong>Maze Map</strong> view and the result will be displayed.</p>
        <p><NavLink tag={Link} className="text-dark" to="/maze-map">Click <strong>here</strong> to visit the Maze map view</NavLink></p>
        </div>
    );
  }
}
