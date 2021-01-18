
import React, { createContext, useContext, useEffect } from 'react';
import { useLocalStore } from "mobx-react-lite";

const MainStoreContext = createContext();

// eslint-disable-next-line react/prop-types
export const MainStoreProvider = ({children}) => {
    const store = useLocalStore( () => ({
        text: "asd",
        token: localStorage.getItem('app_token') || null,
        setToken (value) {
            localStorage.setItem('app_token',value);
            this.token = value;
        },
        setText(value){
            store.text = value || null;
        },
        get isLogin () {
            if(this.token){
                return true;
            }
            return false;
        },
        get textCom (){
            return store.text + " hello";
        }
    }))


  useEffect(() => {
    const onStorage = () => {
        var storage = localStorage.getItem('app_token');
        if(!storage){
            store.setToken(null)
        }
    };
    window.addEventListener("storage", onStorage);
    return () => window.removeEventListener("storage", onStorage);
  }, []);

    return <MainStoreContext.Provider value={store}>{children}</MainStoreContext.Provider>
}

export const useMainStore = () => {
    const store = useContext(MainStoreContext);
    return store;
}


