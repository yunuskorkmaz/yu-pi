import React from "react";
import { DesktopOutlined, TeamOutlined } from "@ant-design/icons";

const routes = [
	{ path: "/ngrok"},
	{ path: "/"},
];

export default routes;

export const menu = [
	{
		name: "Ana Sayfa",
		url: "/",
		icon: <DesktopOutlined />,
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