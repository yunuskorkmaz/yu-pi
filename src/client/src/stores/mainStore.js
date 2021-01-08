
import React, { createContext, useContext } from 'react';
import { useLocalStore } from "mobx-react-lite";

const MainStoreContext = createContext();

// eslint-disable-next-line react/prop-types
export const MainStoreProvider = ({children}) => {
    const store = useLocalStore( () => ({
        text: "asd",
        setText(value){
            store.text = value;
        },
        get textCom (){
            return store.text + " hello";
        }
    }))

    return <MainStoreContext.Provider value={store}>{children}</MainStoreContext.Provider>
}

export const UseMainStore = () => {
    const store = useContext(MainStoreContext);
    return store;
}


