import {Component} from 'react'
import {Container} from 'reactstrap'
import {Outlet} from 'react-router-dom'

export class AdminLayout extends Component{

    render(){
        
        return(
            <div>
                <Container>
                    <h1>This is admin layout!</h1>
                    <Outlet></Outlet>
                </Container>
            </div>
        );
    }
} 