using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons 
{
	public class CrystalFlareStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Crystal Flare Staff");
			// Tooltip.SetDefault("Fires blue frost flames that explode");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 49;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 15;
	        Item.width = 44;
	        Item.height = 46;
	        Item.useTime = 12;
	        Item.useAnimation = 24;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5.25f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
	        Item.UseSound = SoundID.Item20;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("SpiritFlameCurse").Type;
	        Item.shootSpeed = 14f;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "EssenceofEleum", 3);
	        recipe.AddIngredient(ItemID.CrystalShard, 15);
	        recipe.AddIngredient(ItemID.FrostStaff);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}