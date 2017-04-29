var AccountApi = function () {
    var self = this;
    var m = {};

    self.GenerateAuthToken = function (email, password) {
        return ApiPromiser.Call('POST', 'json/account/token', JSON.stringify({
            Email: email,
            Password: password,
        }));
    }

    self.GetAccount = function () {
        return ApiPromiser.Call('GET', 'json/account', null, function (data) {
            return {
                username: data.Username,
                email: data.Email,
            };
        });
    }

    self.CreateUser = function (email, password) {
        return ApiPromiser.Call('POST', 'json/account', JSON.stringify({
            Email: email,
            Password: password
        }), function (data) {
            return data == "true" || data == "True";
        })
    }
}