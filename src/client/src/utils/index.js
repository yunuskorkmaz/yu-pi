const validateToken = (token) => {
    try {
        let base64Url = token.split('.')[1]
        let base64 = decodeURIComponent(
          atob(base64Url)
            .split('')
            .map(function (c) {
              return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
            })
            .join('')
        )
        var parseToken = JSON.parse(base64);
        if(new Date(parseToken.exp) > new Date()){
            return true
        }
        else{
            return false;
        }
    } catch (error) {
        return false;
    }
}

export { validateToken}