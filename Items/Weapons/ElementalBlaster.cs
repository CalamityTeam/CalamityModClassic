using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class ElementalBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Elemental Blaster");
			// Tooltip.SetDefault("Does not consume ammo\nFires a storm of rainbow blasts");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 77;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 104;
			Item.height = 42;
			Item.useTime = 2;
			Item.useAnimation = 6;
			Item.useStyle = 5;
			Item.noMelee = true;
			Item.knockBack = 1.75f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/PlasmaBolt");
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("RainbowBlast").Type;
			Item.shootSpeed = 18f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 0);
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "SpectralstormCannon");
            recipe.AddIngredient(null, "ClockGatlignum");
            recipe.AddIngredient(null, "PaintballBlaster");
            recipe.AddIngredient(null, "GalacticaSingularity", 5);
            recipe.AddIngredient(ItemID.LunarBar, 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
		}
	}
}