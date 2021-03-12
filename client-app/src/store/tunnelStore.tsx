import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { makeAutoObservable } from "mobx"
import { Tunnel } from "../models/Tunnel";

export class TunnelStore {
    tunnels: Array<any> = []
    connection: HubConnection = new HubConnectionBuilder().withUrl(process.env.REACT_APP_REST_URL + 'tunnelHub').withAutomaticReconnect().build();

    constructor() {
        makeAutoObservable(this);
        

        this.connection.start().then(() => {
            this.connection.on('onConnected', (data: Array<Tunnel>) => this.onConnected(data))
            this.connection.on('onTunnelAdded', (data: Tunnel) => this.onTunnelAdded(data));
            this.connection.on('onTunnelUpdated', (data: Tunnel) => this.onTunnelUpdated(data));
            this.connection.on('onTunnelDeleted', (data: boolean) => this.onTunnelDeleted(data))
        }).catch((error) => console.error(error))

    }
    onTunnelDeleted(data: boolean): void {
        console.log("ondeleted" + data)
        var index = this.tunnels.findIndex(a =>  a.id === data);
        this.tunnels.splice(index,1);
    }

    closeTunnel(data:any){
        this.connection.send('TunnelClose',data.id)
    }

    onConnected(data: Array<Tunnel>) {
        this.tunnels = data;
    }

    onTunnelAdded(data: any) {
        console.log("onTunnelAdded",data)
        this.tunnels.push(data);
        console.log("tunnelşs",this.tunnels)
    }

    onTunnelUpdated(data: any) {
        console.log("onTunnelUpdated",data)
        this.tunnels.forEach((value,index) => {
            if(value.id === data.id){
                this.tunnels[index] = data;
            }
        });
    }

    addTunnel(data: any) {
        this.connection.send('AddTunnel', (data));
    }
}