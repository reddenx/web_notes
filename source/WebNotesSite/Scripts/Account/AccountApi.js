var AccountApi = function () {
    var self = this;
    var m = {};

    self.GenerateAuthToken = function (email, password) {
        return ApiPromiser.Call('POST', 'json/account/token', JSON.stringify({
            Email: email,
            Password: password,
        }));
    }
}