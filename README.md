# Diana
Diana是基于 C# 语言的WEB + ORM 轻量级开发框架，其核心设计目的是轻量级、功能强大、易扩展、高度分层隔离、高安全。<br/>

[官网](http://www.systemshell.org:8888/soft/)<br>

开发环境：Visual Studio2012

数据库：Sql Server2008 R2

主要特点：

* 自开发的MVC框架，通过使用单入口模式，利用反射机制实现了模型(model)－视图(view)－控制器(controller)的逻辑、数据、界面显示的分离，使得框架加载性能相对于传统MVC框架较为高效。
* 前台设计了两种页面风格，分别是基于MVVM框架Knockout的ACE前端，基于Bootstrap开发的扁平化前端，界面风格简洁、大气、操作便捷。
* 后台ORM采用SQL Sugar使系统与数据库的操作更加灵活稳定，性能更高。
* 支持SQLServer、MySQL数据库类型，且切换简单，只需在配置文件中切换。
* 基于rbac(role based access control)的形式的权限控制,按角色、按部门、按用户组都可以，操作权限细化到了界面上的每一个按钮，并且将权限细分为静态权限和动态权限，这样将资源和动作分离开，避免程序耦合度高，实现两者的独立运行，以适用于复杂的权限系统。
* 集成工作流引擎组件，支持可视化操作，使业务流程灵活可控。

Diana技术介绍：

后端
* Sql sugar  ORM
* log4net 系统日志
* Newtonsoft.JsonJson处理
* Ninject 依赖注入容器
* Velocity 模板

前端
* JS框架：Jquery-1.10.2.min、jquery-ui
* 前端框架：Bootstrap，knockout
* 数据表格:JqGrid
* 分页插件： pagination
* 布局：Layout
* 图表： echarts
* 字体图片：FontAwesome
* 富文本：Ueditor
* 日期控件：WdataPicker
* 树结构控件：jQueryWTree
* 对话框：layer
* 工作流流程图：myflow.js