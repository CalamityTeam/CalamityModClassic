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
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Leviathan
{
	public class LureofEnthrallment : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Pearl of Enthrallment");
			//Tooltip.SetDefault("Summons a siren lure to fight for you\nThe lure stays above you, shooting water spears, ice mist, and treble clefs at nearby enemies");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 13));
		}

	    public override void SetDefaults()
	    {
	        Item.width = 56;
	        Item.height = 56;
	        Item.value = 500000;
	        Item.rare = ItemRarityID.Pink;
	        Item.accessory = true;
	    }
	    
	    public override void UpdateAccessory(Player player, bool hideVisual)
		{
	    	CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			modPlayer.sirenWaifu = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(Mod.Find<ModBuff>("SirenLure").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("SirenLure").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("SirenLure").Type] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("SirenLure").Type, 0, 0f, Main.myPlayer, 0f, 0f);
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