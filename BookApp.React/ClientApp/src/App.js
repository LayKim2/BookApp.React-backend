import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';

import './custom.css'
import { BooksIndex } from './components/Books/BooksIndex';
import { BooksCreate } from './components/Books/BooksCreate';
import { BooksEdit } from './components/Books/BooksEdit';
import { BooksDelete } from './components/Books/BooksDelete';
import { BooksDetail } from './components/Books/BooksDetail';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                <Route path='/counter' component={Counter} />
                <Route path='/fetch-data' component={FetchData} />
                {/*                <Route path='/Books' component={BooksIndex} exact = "true" />*/}
                <Route path={['/Books', '/Books/Index']} component={BooksIndex} exact="true" />
                <Route path='/Books/Create' component={BooksCreate} exact="true"/>
                <Route path='/Books/Edit/:id' component={BooksEdit} exact="true"/>
                <Route path='/Books/Delete/:id' component={BooksDelete} exact="true" />
                <Route path='/Books/Detail/:id' component={BooksDetail} exact="true" />
            </Layout>
        );
    }
}
