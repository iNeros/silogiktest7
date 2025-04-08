export default {
  dothis() {
    console.log("dothis");
  },

  setCookie(name, value) {
    var expireDate = new Date();
    expireDate.setMonth(expireDate.getMonth() + 3);
    document.cookie = `${name}=${encodeURIComponent(
      value
    )}; expires=${expireDate}; path=/`;
  },

  getCookie(name) {
    const cookies = document.cookie.split(";");
    for (let i = 0; i < cookies.length; i++) {
      let c = cookies[i].trim().split("=");
      if (c[0] === name) {
        return decodeURIComponent(c[1]);
      }
    }
    return "";
  },
};
