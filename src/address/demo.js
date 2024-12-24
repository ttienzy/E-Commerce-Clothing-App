
const provinces = [
    { "code": "01", "name": "Thành phố Hà Nội" },
    { "code": "79", "name": "Thành phố Hồ Chí Minh" },
    { "code": "31", "name": "Thành phố Hải Phòng" },
    { "code": "48", "name": "Thành phố Đà Nẵng" },
    { "code": "92", "name": "Thành phố Cần Thơ" },
    { "code": "02", "name": "Tỉnh Hà Giang" }
];

const Demo = () => {
    return (
        <select>
            <option value="-1" hidden>Select your province</option>
            {provinces.map(item => (<option value={item.code}>{item.name}</option>))}
        </select>
    )
}