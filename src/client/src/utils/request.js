/* eslint-disable no-async-promise-executor */
import { message } from "antd";

/* eslint-disable no-undef */
const post = (path,data,auth = true) => {

    return new Promise( async (resolve,reject) => {
        var header = {
            "Content-Type": 'application/json',
            "Accept": 'application/json'
        };
        if(auth) header["Authorization"] = "Bearer " + localStorage.getItem('app_token');
    
        var result = await fetch(`${process.env.REACT_APP_API}${path}`, {
            method: 'post',
            body: JSON.stringify(data),
            headers: {
               ...header
            }
        });
    
        if(result.status === 422){
            var errors = await result.json();
            errors.errors.map(item => {
                message.error(item.fieldName + ' ' + item.message);
            })
            reject();
        }else if(result.status === 401){
            message.error('Not Found Session')
            browserHistory.push("/login")
            reject();
        }
        else{
            resolve(await result.json());
        }
    })
    
}

const get = (path,auth = true) => {
    return new Promise(async (resolve,reject) => {
        var header = {
            "Content-Type": 'application/json',
            "Accept": 'application/json'
        };
        if(auth) header["Authorization"] = "Bearer " + localStorage.getItem('app_token');
        var result = await fetch(`${process.env.REACT_APP_API}${path}`, {
            method: 'get',
            headers: {
               ...header
            }
        });
        if(result.status === 401){
            message.error('Not Found Session')
            localStorage.removeItem('app_token')
            reject(401);
        }
        else{
            resolve(await result.json());
        }
    })
}
export default {
    post,
    get
}