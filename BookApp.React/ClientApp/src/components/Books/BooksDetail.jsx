import react, { Component } from 'react';
import axios from 'axios';

export class BooksDetail extends Component {
    constructor(props) {
        super(props);

        this.state = {
            title: '',
            description: '',
            created: null,
        };

        this.deleteBy = this.deleteBy.bind(this);
        this.goIndex = this.goIndex.bind(this);

    }

    componentDidMount() {

        const { id } = this.props.match.params;

        axios.get('/api/books/' + id).then(response => {
            let data = response.data;

            this.setState({
                title: data.title,
                description: data.description,
                created: data.created,
            });

        });
    }

    deleteBy(e) {

        e.preventDefault(); // js x react에서만 이 기능을 사용할수 있게 -> 이거 없으면 다른 url로 간다(?)

        const { id } = this.props.match.params;

        axios.delete('/api/books/' + id).then(response => {
            const { history } = this.props;
            history.push("/books");
        });

    }

    goIndex() {
        const { history } = this.props;
        history.push("/Books");
    }

    render() {
        return (
            <>
                <div className="row">
                    <div className="col-md-8">
                        <form>
                            <div className="form-group">
                                <label>Title</label>
                                <input type="text" className="form-control"
                                    value={this.state.title} placeholder="Enter Title"
                                    readOnly
                                />
                            </div>

                            <div className="form-group">
                                <label>Description</label>
                                <input type="text" className="form-control"
                                    value={this.state.description} placeholder="Enter Description"
                                    readOnly
                                />
                            </div>

                            <div className="form-group">
                                <button type="submit" className="btn btn-danger" onClick={this.deleteBy}>Delete</button>
                                &nbsp;
                                <button className="btn btn-secondary" onClick={this.goIndex}>List</button>
                            </div>
                        </form>
                    </div>
                </div>
            </>
        )
    }
}