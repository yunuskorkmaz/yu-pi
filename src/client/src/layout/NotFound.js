import React, { memo } from "react";
import { Result, Button } from "antd";
import { useHistory } from "react-router-dom";

const NotFound = () => {
    const history = useHistory();
  return (
    <div style={{height: '100vh',display:'flex', alignItems:'center',justifyContent:'center'}}>
      <Result
        status="404"
        title="404"
        subTitle="Sorry, the page you visited does not exist."
        extra={<Button onClick={() => history.replace('/')} type="primary">Back Home</Button>}
      />
    </div>
  );
};

export default memo(NotFound);
