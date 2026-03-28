using System;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer;
using CalamityModClassicPreTrailer.Items.CalamityCustomThrowingDamage;

namespace CalamityModClassicPreTrailer.Items.DesertScourge
{
    public class AeroStone : ModItem
    {
    	public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aero Stone");
			/* Tooltip.SetDefault("One of the ancient relics\n" +
            	"Increases movement speed by 10%, jump speed by 200%, and all damage by 3%"); */
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(4, 8));
			ItemID.Sets.AnimatesAsSoul[Type] = true;
		}
    	
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = 5;
			Item.accessory = true;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CalamityCustomThrowingDamagePlayer modPlayer = CalamityCustomThrowingDamagePlayer.ModPlayer(player);
			Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0f, 0.425f, 0.425f);
        	player.moveSpeed += 0.1f;
        	player.jumpSpeedBoost += 2.0f;
			player.GetDamage(DamageClass.Melee) += 0.03f;
			player.GetDamage(DamageClass.Magic) += 0.03f;
			player.GetDamage(DamageClass.Ranged) += 0.03f;
			modPlayer.throwingDamage += 0.03f;
			player.GetDamage(DamageClass.Summon) += 0.03f;
		}
    }
}
