using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.DrawLayers;
    public class MountsAboveOwnerLayer : PlayerDrawLayer
    {
        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.BackAcc);

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            return drawPlayer.mount != null && (drawPlayer.GetModPlayer<CalamityPlayerPreTrailer>().fab ||
                                                drawPlayer.GetModPlayer<CalamityPlayerPreTrailer>().crysthamyr ||
                                                drawPlayer.GetModPlayer<CalamityPlayerPreTrailer>().onyxExcavator);
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            try
            {
                drawInfo.drawPlayer.mount.Draw(drawInfo.DrawDataCache, 3, drawInfo.drawPlayer, drawInfo.Position,
                    drawInfo.colorMount, drawInfo.playerEffect, drawInfo.shadow);
            }
            catch (IndexOutOfRangeException) { }
        }
    }