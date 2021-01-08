// import { useObserver } from 'mobx-react-lite';
import { observer } from 'mobx-react-lite';
import React, { useEffect, useState } from 'react';
import { UseMainStore } from './stores/mainStore';

function App() {

  const store = UseMainStore();
  const [abc, setAbc] = useState("");

  useEffect(() => {
    console.log(store);
  },[store.text])

  const handleClick = () => {
    store.setText(abc)
  }
  return  (
    <div className="App">
      <input type="text" value={abc} onChange={e => setAbc(e.target.value)} />
      <button onClick={() => handleClick()}>Kaydet</button>
      <pre>
        {JSON.stringify(store,null,2)}
      </pre>
      {store.textCom}
    </div>
  ) 
}

export default observer(App);
