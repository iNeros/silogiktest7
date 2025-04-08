let info = {};

info = {
  domainName: window.location.hostname == "localhost" ? "" : "",
  domainNameWithHttp:
    window.location.hostname == "localhost"
      ? "https://localhost:7175/"
      : "https://ineritos31-001-site1.otempurl.com/",
  domainNameForApi:
    window.location.hostname == "localhost"
      ? "https://localhost:7175/api/"
      : "https://ineritos31-001-site1.otempurl.com/api/",
};

export default info;
