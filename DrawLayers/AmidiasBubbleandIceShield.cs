using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.DrawLayers
{
    public class AmidiasBubbleandIceShield : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.BackAcc);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo) =>
            drawInfo.drawPlayer.GetModPlayer<CalamityPlayerPreTrailer>().amidiasBlessing ||
            drawInfo.drawPlayer.GetModPlayer<CalamityPlayerPreTrailer>().sirenIce && drawInfo.shadow == 0f;
        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            CalamityPlayerPreTrailer modPlayer = drawInfo.drawPlayer.GetModPlayer<CalamityPlayerPreTrailer>();
            if (modPlayer.sirenIce)
            {
                Texture2D texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/IceShield").Value;
                int drawX = (int)(drawInfo.Position.X + drawInfo.drawPlayer.width / 2f - Main.screenPosition.X);
                int drawY = (int)(drawInfo.Position.Y + drawInfo.drawPlayer.height / 2f - Main.screenPosition.Y); //4
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, Color.Cyan, 0f,
                    new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
                drawInfo.DrawDataCache.Add(data);
            }
            else if (modPlayer.amidiasBlessing)
            {
                Texture2D texture = ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/ExtraTextures/AmidiasBubble").Value;
                int drawX = (int)(drawInfo.Position.X + drawInfo.drawPlayer.width / 2f - Main.screenPosition.X);
                int drawY = (int)(drawInfo.Position.Y + drawInfo.drawPlayer.height / 2f - Main.screenPosition.Y); //4
                DrawData data = new DrawData(texture, new Vector2(drawX, drawY), null, Color.White, 0f,
                    new Vector2(texture.Width / 2f, texture.Height / 2f), 1f, SpriteEffects.None, 0);
                drawInfo.DrawDataCache.Add(data);
            }
        }
    }
}