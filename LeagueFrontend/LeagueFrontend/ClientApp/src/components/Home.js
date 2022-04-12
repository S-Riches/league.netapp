import React, { Component } from 'react';
//import { ReactDOM } from 'react';


export class Home extends Component {
    static displayName = Home.name;
    static value = '';

    constructor() {
        super();
        this.state = { summonerName: '' }
    }

    // the variable name stores whatever the user is typing which i can use to send requests to the riot api
    render() {
        let name = this.state.summonerName;
        console.log(name);
        return (
          <div>
                <h1>Input your summoner name</h1>
                <div>
                    <input type="text" placeholder="e.g. hideinbrush" value={this.state.summonerName} id="summonerName" onChange={(e) => { this.setState({ summonerName: e.target.value }) }}></input>
                </div>

            </div>
            
            //button isnt neccessary if we just send the value on change, as it rerenders the component.
            //<button onClick={this.sendData} type="button"> Send! </button>
        );
    }
    
}
