var ApiPromiser = {
    Call: function (method, url, data, buildModel) {
        return new ApiPromiser.WrappedApiPromise($.ajax({
            url: url,
            type: method,
            contentType: 'application/json',
            dataType: 'json',
            data: data,
        }), buildModel);
    },
    WrappedApiPromise: function (jqPromise, buildViewmodelFromData) {
        var self = this;
        var m = {};

        m.promise = jqPromise;
        m.sucessCallbacks = [];
        m.buildViewmodelFromData = buildViewmodelFromData;

        self.done = function (callback) {
            if (buildViewmodelFromData) {
                m.sucessCallbacks.push(callback);
            } else {
                m.promise.done(callback);
            }

            return m.promise;
        }

        self.fail = function (callback) {
            m.promise.fail(callback);
            return m.promise;
        }

        self.always = function (callback) {
            m.promise.always(callback);
            return m.promise;
        }

        m.promise.done(function (data) {
            m.sucessCallbacks.forEach(function (callback) {
                callback(m.buildViewmodelFromData(data));
            });
        })
    }
}