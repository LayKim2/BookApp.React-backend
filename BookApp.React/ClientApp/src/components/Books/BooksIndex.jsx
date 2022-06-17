import React, { Component } from 'react';
import axios from 'axios';


export class BooksIndex extends Component {
    constructor(props) {
        super(props);

        this.state = {
            books: [],
            loading: true
        }

        this.goCreatePage = this.goCreatePage.bind(this);
        this.editBy = this.editBy.bind(this);
        this.deleteBy = this.deleteBy.bind(this);
        this.detailBy = this.detailBy.bind(this);

    }

    // page initialized
    componentDidMount() {
        //this.populateBooksData();
        //this.populateBooksDataWithAxios();
        this.populateBooksDataWithAxiosAsync();
    }

    renderBooksTable(books) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Title</th>
                        <th>Description</th>
                        <th>Created</th>
                        <th>Action, Admin</th>
                    </tr>
                </thead>
                <tbody>
                    {books.map(book =>
                        <tr key={book.id} onClick={() => this.detailBy(book.id)}>
                            <td onClick={() => this.detailBy(book.id)} >{book.id}</td>
                            <td onClick={() => this.detailBy(book.id)} >{book.title}</td>
                            <td onClick={() => this.detailBy(book.id)} >{book.description}</td>
                            <td onClick={() => this.detailBy(book.id)} >{book.created ? new Date(book.created).toLocaleDateString() : "-"}</td>
                            <td className="text-nowrap">
                                <button className="btn btn-sm btn-primary" onClick={() => this.editBy(book.id) }>Edit</button>
                                &nbsp;
                                <button className="btn btn-sm btn-danger" onClick={() => this.deleteBy(book.id) }>Delete</button>
                            </td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    goCreatePage() {
        //console.log("create");
        const { history } = this.props;
        // history.goBack(); 이전 페이지로 돌아감 
        // ref : https://gongbu-ing.tistory.com/45
        history.push('/Books/Create');
    }

    editBy(id) {
        //console.log("edit by + " + id);
        const { history } = this.props;
        history.push('/Books/Edit/' + id);
    }

    deleteBy(id) {
        //console.log("delete by + " + id);
        const { history } = this.props;
        history.push('/Books/Delete/' + id);

        //e.preventDefault();

        //if (window.confirm("Do you want to delete?")) {

        //    axios.delete('/api/books/' + id);

        //    this.populateBooksDataWithAxiosAsync();

        //} else {
        //    return false;
        //}
    }

    detailBy(id) {
        const { history } = this.props;
        history.push('/Books/Detail/' + id);
    }

    render() {

        let content = this.state.loading
            ? <p><em>Loading...</em></p>
            : this.renderBooksTable(this.state.books);

        return (
            <div>
                <h1>My Books <button className="btn btn-primary" onClick={this.goCreatePage}><span className="fa fa-plus">+</span></button></h1>
                <h2>제가 집필한 책입니다.</h2>
                {content}
            </div>
        );
    }

    // Fetch API
    async populateBooksData() {
        const response = await fetch("/api/books");
        const data = await response.json();
        this.setState({ books: data, loading: false });
    }

    // Axios 2가지 방식인데 둘다 async라고함
    // 첫번째 방식
    populateBooksDataWithAxios() {
        axios.get("/api/books").then(response => {
            const data = response.data;
            this.setState({ books: data, loading: false });
        });
    }

    // 두번째 방식
    async populateBooksDataWithAxiosAsync() {
        const response = await axios.get("/api/books");
        const data = response.data;
        this.setState({ books: data, loading: false });
    }
}