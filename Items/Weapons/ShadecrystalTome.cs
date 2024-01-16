using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ShadecrystalTome : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/ShadecrystalTome");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Shadecrystal Barrage");
        Item.damage = 27;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 4;
        Item.width = 28;
        Item.height = 30;
        Item.useTime = 6;
        Item.useAnimation = 6;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Summons rapid fire shadecrystals, can shoot two crystals at once");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 5.5f;
        Item.value = 550000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item9;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("ShadecrystalProjectile").Type;
        Item.shootSpeed = 16f;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
		float SpeedA = velocity.X;
   		float SpeedB = velocity.Y;
        int num6 = Main.rand.Next(1, 3);
        for (int index = 0; index < num6; ++index)
        {
      	 	float num7 = velocity.X;
            float num8 = velocity.Y;
            float SpeedX = velocity.X + (float) Main.rand.Next(-40, 41) * 0.05f;
            float SpeedY = velocity.Y + (float) Main.rand.Next(-40, 41) * 0.05f;
            Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
        }
        return false;
	}
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(ItemID.CrystalStorm);
		recipe.AddIngredient(null, "Stardust", 20);
		recipe.AddIngredient(null, "VerstaltiteBar", 3);
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
	}
}}