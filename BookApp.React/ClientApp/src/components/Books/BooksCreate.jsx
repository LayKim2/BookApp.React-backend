import React, { Component } from 'react';
import axios from 'axios';

export class BooksCreate extends Component {
    constructor(props) {
        super(props);

        this.state = {
            title: '',
            description: '',
            created: null,
        };

        // event binding
        this.handleChangeTitle = this.handleChangeTitle.bind(this);
        //this.handleChangeDescription = this.handleChangeDescription.bind(this);

        this.handleSubmit = this.handleSubmit.bind(this);
        this.goIndex = this.goIndex.bind(this);

    }

    // [1] 함수로 이벤트 처리기 만들고 생성자에 바인딩
    handleChangeTitle(e) {
        this.setState({
            title: e.target.value
        });
    }

    // [2] 화살표 함수 (람다 식) 으로 event binding
    handleChangeDescription = (event) => {
        this.setState({
            description: event.target.value
        });
    }

    goIndex() {
        const { history } = this.props;
        history.push("/Books");
    }

    handleSubmit(e) {
        e.preventDefault(); // js x react에서만 이 기능을 사용할수 있게

        let bookDto = {
            title: this.state.title,
            description: this.state.description,
        };

        axios.post("/api/books", bookDto).then(result => {
            this.goIndex();
        });

    }

    render() {
        return (
            <>
                <div className="row">
                    <div className="col-md-8">
                        <form onSubmit={this.handleSubmit}>
                            <div className="form-group">
                                <label>Title</label>
                                <input type="text" className="form-control"
                                    value={this.state.title} placeholder="Enter Title"
                                    onChange={this.handleChangeTitle}
                                />
                            </div>

                            <div className="form-group">
                                <label>Description</label>
                                <input type="text" className="form-control"
                                    value={this.state.description} placeholder="Enter Description"
                                    onChange={this.handleChangeDescription}
                                />
                            </div>

                            <div className="form-group">
                                <button type="submit" className="btn btn-primary">Submit</button>
                                &nbsp;
                                <button className="btn btn-secondary" onClick={this.goIndex}>List</button>
                            </div>
                        </form>
                    </div>
                </div>
            </>
        );
    }
}