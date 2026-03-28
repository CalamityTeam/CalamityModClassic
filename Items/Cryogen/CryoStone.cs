using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.Cryogen
{
    public class CryoStone : ModItem
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cryo Stone");
			/* Tooltip.SetDefault("One of the ancient relics\n" +
            	"Increases damage reduction by 5% and all damage by 3%"); */
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 5));
			ItemID.Sets.AnimatesAsSoul[Type] = true;
		}
    	
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 5;
            Item.defense = 6;
			Item.accessory = true;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
        	Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0f, 0.25f, 0.6f);
			player.endurance += 0.05f;
			player.GetDamage(DamageClass.Melee) += 0.03f;
			player.GetDamage(DamageClass.Magic) += 0.03f;
			player.GetDamage(DamageClass.Ranged) += 0.03f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.03f;
			player.GetDamage(DamageClass.Summon) += 0.03f;
		}
    }
}
