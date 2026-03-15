using System.Collections.Generic;
using System.Web;

namespace PillIdentifierMVC.Helpers
{
    public static class IconHelper
    {
        // =====================================================================
        // SHAPE ICONS — inline SVG, 22x22, gray fill + dark stroke
        // =====================================================================
        public static IHtmlString ShapeIcon(int id)
        {
            string inner;
            switch (id)
            {
                case 1:  // Hình tròn
                    inner = "<circle cx='11' cy='11' r='9' fill='#e0e0e0' stroke='#666' stroke-width='1.5'/>";
                    break;
                case 3:  // Hình bầu dục
                    inner = "<ellipse cx='11' cy='11' rx='9' ry='6' fill='#e0e0e0' stroke='#666' stroke-width='1.5'/>";
                    break;
                case 4:  // Hình tam giác
                    inner = "<polygon points='11,2 20,20 2,20' fill='#e0e0e0' stroke='#666' stroke-width='1.5' stroke-linejoin='round'/>";
                    break;
                case 5:  // Hình vuông
                    inner = "<rect x='2' y='2' width='18' height='18' fill='#e0e0e0' stroke='#666' stroke-width='1.5'/>";
                    break;
                case 6:  // Hình lục giác
                    inner = "<polygon points='11,1 20,6 20,16 11,21 2,16 2,6' fill='#e0e0e0' stroke='#666' stroke-width='1.5' stroke-linejoin='round'/>";
                    break;
                case 7:  // Hình khác
                    inner = "<circle cx='11' cy='11' r='9' fill='#e0e0e0' stroke='#666' stroke-width='1.5' stroke-dasharray='3,2'/>";
                    break;
                case 8:  // Hình thuôn dài (capsule)
                    inner = "<rect x='1' y='6' width='20' height='10' rx='5' fill='#e0e0e0' stroke='#666' stroke-width='1.5'/>";
                    break;
                case 12: // Hình chữ nhật
                    inner = "<rect x='1' y='5' width='20' height='12' fill='#e0e0e0' stroke='#666' stroke-width='1.5'/>";
                    break;
                case 13: // Hình thoi
                    inner = "<polygon points='11,1 21,11 11,21 1,11' fill='#e0e0e0' stroke='#666' stroke-width='1.5' stroke-linejoin='round'/>";
                    break;
                case 14: // Hình ngũ giác (pentagon)
                    inner = "<polygon points='11,2 20,8 16,19 6,19 2,8' fill='#e0e0e0' stroke='#666' stroke-width='1.5' stroke-linejoin='round'/>";
                    break;
                case 15: // Hình thất giác (heptagon)
                    inner = "<polygon points='11,2 18,5 20,13 15,19 7,19 2,13 4,5' fill='#e0e0e0' stroke='#666' stroke-width='1.5' stroke-linejoin='round'/>";
                    break;
                case 16: // Hình bát giác (octagon)
                    inner = "<polygon points='11,2 17,5 20,11 17,17 11,20 5,17 2,11 5,5' fill='#e0e0e0' stroke='#666' stroke-width='1.5' stroke-linejoin='round'/>";
                    break;
                default:
                    return new HtmlString("");
            }
            return new HtmlString(
                "<svg xmlns='http://www.w3.org/2000/svg' width='22' height='22' viewBox='0 0 22 22' " +
                "style='vertical-align:middle;margin-right:6px;flex-shrink:0;'>" +
                inner + "</svg>");
        }

        // =====================================================================
        // COLOR CIRCLES — CSS colored circle spans
        // =====================================================================
        private static readonly Dictionary<int, string> ColorHex = new Dictionary<int, string>
        {
            { 1,  "#E53935" },  // Đỏ
            { 2,  "#FB8C00" },  // Cam
            { 4,  "#FDD835" },  // Vàng
            { 5,  "#29B6F6" },  // Lam
            { 6,  "#3949AB" },  // Chàm
            { 7,  "#8E24AA" },  // Tím
            { 8,  "#F5F5F5" },  // Trắng
            { 9,  "#1E88E5" },  // Xanh dương
            { 36, "#9E9E9E" },  // Khác
            { 37, "#F06292" },  // Hồng
            { 38, "#66BB6A" },  // Xanh lá mạ
        };

        public static IHtmlString ColorCircle(int id)
        {
            string hex;
            if (!ColorHex.TryGetValue(id, out hex)) return new HtmlString("");
            string border = id == 8 ? "border:1px solid #ccc;" : "border:1px solid rgba(0,0,0,0.1);";
            return new HtmlString(
                $"<span style='display:inline-block;width:16px;height:16px;border-radius:50%;" +
                $"background:{hex};{border}vertical-align:middle;margin-right:6px;flex-shrink:0;'></span>");
        }

        // =====================================================================
        // GROOVE ICONS — filled gray circle + white score lines
        // =====================================================================
        public static IHtmlString GrooveIcon(int id)
        {
            string lines;
            switch (id)
            {
                case 1:  // Không có rãnh — plain circle
                    lines = "";
                    break;
                case 18: // Một rãnh giữa — full vertical line
                    lines = "<line x1='11' y1='2' x2='11' y2='20' stroke='white' stroke-width='2.5'/>";
                    break;
                case 19: // Rãnh một phần — half vertical line (top)
                    lines = "<line x1='11' y1='2' x2='11' y2='11' stroke='white' stroke-width='2.5'/>";
                    break;
                case 20: // Nhiều rãnh chia — cross
                    lines = "<line x1='11' y1='2' x2='11' y2='20' stroke='white' stroke-width='2.5'/>" +
                            "<line x1='2' y1='11' x2='20' y2='11' stroke='white' stroke-width='2.5'/>";
                    break;
                default:
                    return new HtmlString("");
            }
            return new HtmlString(
                "<svg xmlns='http://www.w3.org/2000/svg' width='22' height='22' viewBox='0 0 22 22' " +
                "style='vertical-align:middle;margin-right:6px;flex-shrink:0;'>" +
                "<circle cx='11' cy='11' r='9' fill='#aaa'/>" +
                lines + "</svg>");
        }
    }
}
