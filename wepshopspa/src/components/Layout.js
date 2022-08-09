import {Component, } from 'react'
import {Container} from 'reactstrap'
import {Outlet} from 'react-router-dom'
export class Layout extends Component{

    render(){
        
        return(
            <Container>
                <h1>This is normal layout!</h1>
                <Outlet></Outlet>
            </Container>
        );
    }
} 