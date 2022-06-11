import React, { Component } from 'react';
import axios from 'axios';

export class Login extends Component {
    constructor(props) {
        super(props);

        this.state = {
            id: '',
            pass: '',
            created: null,
        };

        // event binding
        this.handleChangeID = this.handleChangeID.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        this.goHome = this.goHome.bind(this);

    }

    // [1] 함수로 이벤트 처리기 만들고 생성자에 바인딩
    handleChangeID(e) {
        this.setState({
            id: e.target.value   
        });
    }

    // [2] 화살표 함수 (람다 식) 으로 event binding
    handleChangePw = (event) => {
        this.setState({
            pass: event.target.value
        });
    }

    goHome() {
        const { history } = this.props;
        history.push("/");
    }

    async handleSubmit(e) {
        e.preventDefault(); // js x react에서만 이 기능을 사용할수 있게
        //const created = DateTime.now

        let haruUserDto = {
            userid: this.state.id,
            pass: this.state.pass
        };

        await axios({
            method: 'put',
            url: '/api/account',
            data: haruUserDto
        }).then(result => {
            console.log(result.data);
        });

        //await axios.put("/api/account", haruUserDto).then(result => {
        //    this.goHome();
        //});

        //e.preventDefault(); // js x react에서만 이 기능을 사용할수 있게

        //let bookDto = {
        //    title: this.state.title,
        //    description: this.state.description,
        //};

        //axios.post("/api/books", bookDto).then(result => {
        //    //this.goIndex();
        //});

    }

    render() {
        return (
            <>
                <div className="row">
                    <div className="col-md-8">
                        <form onSubmit={this.handleSubmit}>
                            <div className="form-group">
                                <label>ID</label>
                                <input type="text" className="form-control"
                                    value={this.state.title} placeholder="Enter ID"
                                    onChange={this.handleChangeID}
                                />
                            </div>

                            <div className="form-group">
                                <label>Password</label>
                                <input type="password" className="form-control"
                                    value={this.state.description} placeholder="Enter Password"
                                    onChange={this.handleChangePw}
                                />
                            </div>

                            < br/>
                            <div className="form-group">
                                <button type="submit" className="btn btn-primary">Login</button>
                                &nbsp;
                                <button className="btn btn-secondary" onClick={this.goHome}>Home</button>
                            </div>
                        </form>
                    </div>
                </div>
            </>
        );
    }
}