import { reactive } from 'vue';

// Khai báo biến overlay và khởi tạo với giá trị ban đầu là false
export const overlay = reactive({ value: false });
export const Alert = reactive({
    type: "",
    message:""
});

export function valueAlert(type, message) {
    Alert.type = type;
    Alert.message = message;
}

export const token = () => {
    var name = "_keytoken=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var cookieArray = decodedCookie.split(';');
    for (var i = 0; i < cookieArray.length; i++) {
        var cookie = cookieArray[i];
        while (cookie.charAt(0) === ' ') {
            cookie = cookie.substring(1);
        }
        if (cookie.indexOf(tokenkey) === 0) {
            return cookie.substring(tokenkey.length, cookie.length);
        }
    }
    return "";
}

export const tokenkey = reactive("_keytoken=");