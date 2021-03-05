
export interface Tunnel {
    Id: number
    Name: string
    Protokol: string
    Port: string
    publicUrl: string
    Status: TunnelStatus
}

export enum TunnelStatus {
    Passive = 0,
    Preparing = 1,
    Active = 2
}
