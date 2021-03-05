import React from "react";
import { DesktopOutlined, TeamOutlined } from "@ant-design/icons";


interface IMenu {
	name: string,
	url: string,
	icon?: any,
	items?: Array<IMenu>
}


const routes = [
	{ path: "/tunnels" },
	{ path: "/" },
];

export default routes;

export const menu: Array<IMenu> = [
	{
		name: "Ana Sayfa",
		url: "/",
		icon: <DesktopOutlined/>,

	},
	{
		name: "Tunnels",
		url: "/tunnels",
		icon: <DesktopOutlined/>,

	},
	// {
	// 	name: "Ngrok",
	// 	icon: <UserOutlined />,
	// 	items: [
	// 		{
	// 			name: "Test 1.1",
	// 			url: "/test",
	// 			icon: "",
	// 		},
	// 		{
	// 			name: "Test 1.2",
	// 			url: "/",
	// 			icon: "",
	// 		},
	// 	],
	// },
	{
		name: "Ngrok",
		url: "/ngrok",
		icon: <TeamOutlined />,
	}
];