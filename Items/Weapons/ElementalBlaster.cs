using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class ElementalBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Elemental Blaster");
			//Tooltip.SetDefault("Does not consume ammo\nFires a storm of rainbow blasts");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 110;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 104;
			Item.height = 42;
			Item.useTime = 2;
			Item.useAnimation = 6;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1.75f;
			Item.value = 1000000;
			Item.UseSound = new Terraria.Audio.SoundStyle("CalamityModClassic1Point1/Sounds/Item/PlasmaBolt");
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("RainbowBlast").Type;
			Item.shootSpeed = 24f;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 0);
		}
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(0, 255, 200);
	            }
	        }
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