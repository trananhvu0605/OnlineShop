﻿var effectA = document.querySelector('.thanhtoan');
effectA.href = "/Cart/Order?sumTotal=" + document.querySelector('.sumTotal').textContent + "&sumNumbers=" + document.querySelector('.sumNumbers').textContent;