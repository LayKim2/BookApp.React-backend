import React, { Component } from 'react';
import axios from 'axios';
import KakaoLogin from './Kakao/KakaoLogin';

export class Register extends Component {
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
            pass: this.state.pass,
            salt: null,
            created: null
        };

        await axios.post("/api/account", haruUserDto).then(result => {
            if (result.data == true) {
                this.goHome();
                alert('successed register.')
            }
            else {
                alert('failed register');
            }
        });


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
                                <button type="submit" className="btn btn-primary">Register</button>
                                &nbsp;
                                <button className="btn btn-secondary" onClick={this.goHome}>Home</button>
                            </div>
                        </form>

                        <br />

                        <KakaoLogin/>

                    </div>
                </div>
            </>
        );
    }
}