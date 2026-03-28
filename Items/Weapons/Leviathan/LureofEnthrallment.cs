using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.IO;
using Terraria.ObjectData;
using Terraria.Utilities;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.Leviathan
{
	public class LureofEnthrallment : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Pearl of Enthrallment");
			// Tooltip.SetDefault("Summons a siren to fight for you\nThe siren stays above you, shooting water spears, ice mist, and treble clefs at nearby enemies");
		}

	    public override void SetDefaults()
	    {
	        Item.width = 56;
	        Item.height = 56;
            Item.value = Item.buyPrice(0, 30, 0, 0);
            Item.rare = 7;
	        Item.accessory = true;
	    }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
            if (modPlayer.elementalHeart)
            {
                return false;
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
		{
	    	CalamityPlayerPreTrailer modPlayer = player.GetModPlayer<CalamityPlayerPreTrailer>();
			modPlayer.sirenWaifu = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(Mod.Find<ModBuff>("SirenLure").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("SirenLure").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SirenLure").Type] < 1)
				{
					Projectile.NewProjectile(Entity.GetSource_FromThis(null),player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("SirenLure").Type, (int)(65f * player.GetDamage(DamageClass.Summon).Multiplicative), 2f, Main.myPlayer, 0f, 0f);
				}
			}
		}

        public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "IOU");
	        recipe.AddIngredient(null, "LivingShard");
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}