import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { MazeMap } from './components/MazeMap';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  // exact path = '/' sets the page to be displayed as default in the Layout container when using this.props.children
  render () {
      return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/maze-map' component={MazeMap} />
      </Layout>
    );
  }
}
