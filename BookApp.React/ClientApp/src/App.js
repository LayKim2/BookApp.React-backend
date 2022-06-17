import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';

import './custom.css'
import { BooksIndex } from './components/Books/BooksIndex';
import { BooksCreate } from './components/Books/BooksCreate';
import { BooksEdit } from './components/Books/BooksEdit';
import { BooksDelete } from './components/Books/BooksDelete';
import { BooksDetail } from './components/Books/BooksDetail';

import { Register } from './components/Account/Register';
import { Login } from './components/Account/Login';


import Auth from "./components/Account/Kakao/Auth";
import Profile from "./components/Account/Kakao/Profile";

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Route exact path='/' component={Home} />
                {/*<Route path='/counter' component={Counter} />*/}
                {/*<Route path='/fetch-data' component={FetchData} />*/}
                {/*                <Route path='/Books' component={BooksIndex} exact = "true" />*/}
                <Route path={['/Books', '/Books/Index']} component={BooksIndex} exact="true" />
                <Route path='/Books/Create' component={BooksCreate} exact="true"/>
                <Route path='/Books/Edit/:id' component={BooksEdit} exact="true"/>
                <Route path='/Books/Delete/:id' component={BooksDelete} exact="true" />
                <Route path='/Books/Detail/:id' component={BooksDetail} exact="true" />
                <Route path='/Register' component={Register} exact="true" />
                <Route path='/Login' component={Login} exact="true" />

                {/* kakao */}
                <Route path="/KakaoLogin/Callback">
                    <Auth />
                </Route>
                <Route path="/profile">
                    <Profile />
                </Route>
                {/* kakao */}

            </Layout>
        );
    }
}
