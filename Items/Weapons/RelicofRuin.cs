using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class RelicofRuin : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/RelicofRuin");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Relic of Ruin");
        Item.damage = 36;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 16;
        Item.width = 28;
        Item.height = 32;
        Item.useTime = 35;
        Item.useAnimation = 35;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4.25f;
        Item.value = 350000;
        Item.rare = 5;
        Item.UseSound = SoundID.Item84;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("ForbiddenAxeBlade").Type;
        Item.shootSpeed = 30f;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	float spread = 22.5f * 0.0174f;
    	double startAngle = Math.Atan2(velocity.X, velocity.Y)- spread/2;
    	double deltaAngle = spread/8f;
    	double offsetAngle;
    	int i;
    	for (i = 0; i < 8; i++ )
    	{
   			offsetAngle = (startAngle + deltaAngle * ( i + i * i ) / 2f ) + 32f * i;
        	Projectile.NewProjectile(source, position.X, position.Y, (float)( Math.Sin(offsetAngle) * 5f ), (float)( Math.Cos(offsetAngle) * 5f ), type, damage, knockback, Main.myPlayer);
        	Projectile.NewProjectile(source, position.X, position.Y, (float)( -Math.Sin(offsetAngle) * 5f ), (float)( -Math.Cos(offsetAngle) * 5f ), type, damage, knockback, Main.myPlayer);
    	}
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
        recipe.AddIngredient(ItemID.SpellTome);
        recipe.AddTile(TileID.Bookcases);
        recipe.Register();
    }
}}